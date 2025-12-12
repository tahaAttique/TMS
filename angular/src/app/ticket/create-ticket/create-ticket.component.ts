import { ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CurrentUserLookUpDto, TicketCategoryLookUpDto, TicketDto, TicketService } from '@proxy/tickets';
import { FileAttachmentService } from '../ticket-model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-ticket',
  templateUrl: './create-ticket.component.html',
  styleUrls: ['./create-ticket.component.scss'],
})
export class CreateTicketComponent implements OnInit{
  osList = [
    { label: 'Windows', value: 'windows' },
    { label: 'Linux', value: 'linux' },
    { label: 'macOS', value: 'macos' },
    { label: 'Other', value: 'other' },
  ];

  ticketCategories: TicketCategoryLookUpDto[] = [];
  currentUsers: CurrentUserLookUpDto[] = [];

  descriptionEditorContent: string = '';
  expectedBehaviorEditorContent: string = '';
  actualBehaviorEditorContent: string = '';
  nonWorkRoundEditorContent: string = '';
  stepsToReproduceEditorContent: string = '';
  selectedDescriptionTabIndex: number = 0;
  selectedBehaviorTabIndex: number = 0;
  selectedactualBehaviorTabIndex: number = 0;
  selectednonWorkRoundTabIndex: number = 0;
  selectedStepsToReproduceTabIndex: number = 0;

  form: FormGroup;
  selectedTicket = {} as TicketDto;

  constructor(
    private ticketService: TicketService,
    private fb: FormBuilder,
    private toaster: ToasterService,
    private route: Router
  ) {}
  ngOnInit(): void {
    this.fetchTicketCategories();
    this.buildForm();
  }

  fetchTicketCategories() {
    this.ticketService.getTicketCategoryLookUp().subscribe(items => {
      this.ticketCategories = items;
    });
  }

  createTicket() {
    this.selectedTicket = {} as TicketDto;
    this.buildForm();
  }

  buildForm() {
    this.form = this.fb.group({
      title: [ this.selectedTicket.title || '', Validators.required],
      description: [ this.selectedTicket.description || '', Validators.required],
      expectedBehaviour: [ this.selectedTicket.expectedBehaviour || ''],
      actualBehaviour: [ this.selectedTicket.actualBehaviour || ''],
      knownWorkRound: [ this.selectedTicket.knownWorkRound || ''],
      stepsToReproduce: [ this.selectedTicket.stepsToReproduce || ''],
      operatingSystem: [ this.selectedTicket.operatingSystem || '', Validators.required],
      ticketCategoryId: [ this.selectedTicket.ticketCategoryId || '', Validators.required]
    })
  }

  save() {
    if(this.form.invalid) {
      return;
    }
    
    this.ticketService.create(this.form.value).subscribe(() => {
      this.form.reset();
      this.toaster.success('Ticket Created SuccessFully!');

      this.route.navigate(['/tickets']);
    });
  }

  }
