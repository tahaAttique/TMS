import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, ElementRef, OnInit, viewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CommentDto, CommentService } from '@proxy/comments';
import { priorityTypeOptions, StatusType, statusTypeOptions, TicketDto } from '@proxy/tickets';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.scss'
})
export class CommentComponent implements OnInit{

  commentEditorContent: string = '';
  selectedCommentTabIndex: number = 0;

  statusType = statusTypeOptions

  id: string;
  form: FormGroup;

  selectedComment = {} as CommentDto;
  ticket:  TicketDto ={};
  comments: CommentDto [] = [];
  newComments: CommentDto;

  isEditing: boolean = false

  constructor(
    private route: ActivatedRoute,
    private commentService: CommentService,
    private fb: FormBuilder,
    private toaster: ToasterService,
    private confirmation: ConfirmationService
  ){}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });
    this.getTicketDetail(this.id);
    this.getCommenstByTicketId();
    this.buildForm();
  }

  getTicketDetail(ticketId: string) {
    this.commentService.getDetailOfTicket(ticketId).subscribe((res) => {
      this.ticket = res;
    })
  }

  getCommenstByTicketId() {
    this.commentService.getCommentsByTicketId(this.id).subscribe((res) => {
      this.comments = res;
    })
  }

  updateStatus(statusType: StatusType): void {
    this.ticket.statusType = statusType;
    this.confirmation.warn('::AreYouSureToChangeStatus?', '::AreYouSure?').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.commentService.updateStatus(this.id, statusType).subscribe(
          () => {
            this.toaster.success('Ticket status updated successfully!');
            this.getCommenstByTicketId();
          },
          () => {
            this.toaster.error('Failed to update ticket status. Please try again.');
          }
        );
      }
    });
  }
  
  

  create() {
    this.isEditing = false;
    this.selectedComment = {} as CommentDto;
    this.buildForm(); 
  }

  editComment(id: string) {
    this.commentService.get(id).subscribe((comment) => {
      this.selectedComment = comment;
      this.commentEditorContent = comment.detail;
      this.selectedCommentTabIndex = 0;
      this.buildForm();
    });
  }
  

  buildForm() {
    this.form = this.fb.group({
      ticketId:[this.id ||'', Validators.required ],
      detail: [this.selectedComment?.detail || '', Validators.required]
    })
  }

  save() {
    debugger;
    if (this.form.invalid) {
      return;
    }
  
    if (this.selectedComment.id) {
      this.commentService.update(this.selectedComment.id, this.form.value).subscribe(() => {
        this.getCommenstByTicketId();
      this.toaster.success('Comment Updated Successfully!');
      this.selectedComment = {} as CommentDto;
      this.buildForm(); 
      });
    } else {
      this.commentService.create(this.form.value).subscribe(res  => {
        this.getCommenstByTicketId();
        this.toaster.success('Comment Added Successfully!');
        this.selectedComment = {} as CommentDto;
        this.buildForm();
      });
    }
  
    this.selectedCommentTabIndex = 0;
  }
  

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete?', '::AreYouSure?').subscribe((status) => {
      if( status == Confirmation.Status.confirm) {
        this.commentService.delete(id).subscribe(() => {
          this.toaster.success('Comment Deleted!');
          this.getCommenstByTicketId();
        });
      }
    })
  }

}
