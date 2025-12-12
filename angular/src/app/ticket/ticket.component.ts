import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetTicketListDto, KeyLookUpDto, priorityTypeOptions, statusTypeOptions, TicketCategoryLookUpDto, TicketDto, TicketService } from '@proxy/tickets';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrl: './ticket.component.scss',
  providers: [ListService]
})
export class TicketComponent implements OnInit{
  ticket = {items:[], totalCount: 0} as PagedResultDto<TicketDto>
  ticketCategories: TicketCategoryLookUpDto[] = [];
  agents: KeyLookUpDto[] = [];
  customers: KeyLookUpDto[] = [];
  admins: KeyLookUpDto[] = [];

  statusType = statusTypeOptions
  priorityType = priorityTypeOptions

  file: File = null;
  selectedFile: File | null = null;
  formData: FormData;
  imageSrc: string | ArrayBuffer | null = null;

  filterForm: FormGroup;
  form: FormGroup;
  assignTicketForm: FormGroup;

  filters = {} as GetTicketListDto;

  showFilter = false;
  isAssignOpen = false;

  pageSize = 10;
  currentPage = 1;

  statusInfo = [
    { type: 1, name: 'New', class: 'bg-primary', tooltipColor: 'blue' },
    { type: 2, name: 'Open', class: 'bg-warning', tooltipColor: 'orange' },
    { type: 3, name: 'InProgress', class: 'bg-info', tooltipColor: 'cyan' },
    { type: 4, name: 'Resolved', class: 'bg-success', tooltipColor: 'green' },
    { type: 5, name: 'Closed', class: 'bg-dark', tooltipColor: 'black' },
    { type: 6, name: 'Pending', class: 'bg-secondary', tooltipColor: 'gray' },
    { type: 7, name: 'OnHold', class: 'bg-light', tooltipColor: 'lightgray' },
    { type: 8, name: 'Cancelled', class: 'bg-danger', tooltipColor: 'red' }
  ];

  selectedTicket = {} as TicketDto;
  constructor(
    public readonly list: ListService,
    private ticketService: TicketService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private toaster: ToasterService,
    private router: Router
  ){}

  ngOnInit(): void {
    const ticketStreamCreator = (query) => this.ticketService.getList({...query, ...this.filters});

    this.list.hookToQuery(ticketStreamCreator).subscribe((response) =>
    {
      this.ticket = response;
    });
    this.fetchTicketCategories();
    this.fetchAgents();
    this.fetchAdmins();
    this.fetchCustomers();
  }

  fetchTicketCategories() {
    this.ticketService.getTicketCategoryLookUp().subscribe(items => {
      this.ticketCategories = items;
    });
  }

  fetchAgents() {
    this.ticketService.getAgentKeyUpDto().subscribe(items => {
      this.agents = items;
    });
  }

  fetchCustomers() {
    this.ticketService.getCustomerKeyLookUpDto().subscribe(items => {
      this.customers = items;
    });
  }

  fetchAdmins() {
    this.ticketService.getAdminKeyLookUpDto().subscribe(items => {
      this.admins = items;
    });
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.filters.skipCount = (page - 1) * this.pageSize;
    this.filters.maxResultCount = this.pageSize; 
    this.list.get();
  }

  createTicket() {
    this.selectedTicket = {} as TicketDto;
    this.buildForm();
  }

  editTicket(id: string) {
    this.ticketService.get(id).subscribe((ticket) => {
      this.selectedTicket = ticket;
      this.buildForm();
    })
  }

  navigateToTicketDetails() {
    this.router.navigate(['/ticket-Details']);
  }

  onAssignTicket(){
    if(this.assignTicketForm.invalid) {
      this.toaster.error('Please fill all required fields');
      return;
    }

    const { assignToAgentId, priorityType} = this.assignTicketForm.value;

    this.ticketService.assignTicketToUser(this.selectedTicket.id, assignToAgentId, priorityType)
        .subscribe(() => {
          this.toaster.success('Ticket assigned successfully!');
          this.isAssignOpen = false;
          this.list.get();
        })
  }

  buildForm() {
    this.form = this.fb.group({
      title: [this.selectedTicket.title || '', Validators.required],
      description: [this.selectedTicket.description || '', Validators.required],
      ticketCategoryId: [this.selectedTicket.ticketCategoryId || '', Validators.required]
    })
  }

  buildAssignForm() {
    this.assignTicketForm = this.fb.group({
      assignToAgentId: ['', Validators.required],
      priorityType: ['', Validators.required],
    })
  }

  openAssignModal(ticket: TicketDto) {
    this.selectedTicket = ticket;
    this.buildAssignForm();
    this.isAssignOpen = true;
  }

  onFileSelect(event: any) {
    this.file = (event.target as HTMLInputElement).files[0];
    const file = event.target.files[0];
    this.selectedFile = file ? file : null;
    this.formData = new FormData();
    if (this.selectedFile) {
      this.formData.append('file', this.selectedFile);
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.imageSrc = e.target.result;
      };
      reader.readAsDataURL(this.selectedFile);
    } else {
      this.imageSrc = null;
    }
  }

  uploadImage(ticketId: string) {
    if(this.selectedFile) {
      const fileData = new FormData();
      fileData.append('file', this.selectedFile);

      // this.fileAttachmentService.uploadFile(fileData, ticketId).subscribe(()=> {
      //   this.toaster.success('Image Upload Successfully!');
      // });
    }
  }

  save() {
    if(this.form.invalid) {
      return;
    }
    const formData = new FormData();

    if(this.selectedTicket.id) {
      this.ticketService.update(this.selectedTicket.id, this.form.value).subscribe(() => {
        this.form.reset();
        this.toaster.success('Ticket Updated!');
        this.list.get();
      });
    }
    else {
      this.ticketService.create(this.form.value).subscribe((newTicket) => {
        if(this.selectedFile) {
          this.uploadImage(newTicket.id);
        }
        this.form.reset();
        this.toaster.success('Ticket Added!');
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete?', '::AreYouSure?').subscribe((status) => {
      if(status == Confirmation.Status.confirm) {
        this.ticketService.delete(id).subscribe(() => {
          this.toaster.success('Ticket Deleted!');
          this.list.get();
        });
      }
    });
  }

  clearFilter() {
    this.filters = {} as GetTicketListDto;
    this.list.get();
    this.filterForm.reset();
  }

  getStatusInfo(statusType: number) : { name: string; class: string; tooltip: string; tooltipColor: string } {
    const status = this.statusInfo.find(s => s.type === statusType);
    if (status) {
      return { ...status, tooltip: `${status.name}` };
    }
    return { name: 'New', class: 'bg-primary', tooltip: 'New', tooltipColor: 'blue' };
  }

  nagivateToCreateTicket(){
    this.router.navigate(['/create-ticket']);
  }

  nagivateoComment(id: number) {
    this.router.navigate(['/comments', id]);
  }
}
  
