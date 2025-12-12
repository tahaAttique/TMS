import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetTicketCategoryListDto, TicketCategoryDto, TicketCategoryService } from '@proxy/ticket-categories';

@Component({
  selector: 'app-ticket-category',
  templateUrl: './ticket-category.component.html',
  styleUrl: './ticket-category.component.scss',
  providers: [ListService]
})
export class TicketCategoryComponent implements OnInit{

  category = {items: [], totalCount: 0} as PagedResultDto<TicketCategoryDto>;

  isModalOpen = false;
  showFilter = false;

  form: FormGroup;

  selectedCategory = {} as TicketCategoryDto;
  filters = {} as GetTicketCategoryListDto;

  constructor(
    public readonly list: ListService,
    private categoryService: TicketCategoryService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private toaster: ToasterService
  ){}

  ngOnInit(): void {
    const categoryStreamCreator = (query) => this.categoryService.getList({ ...query, ...this.filters});

    this.list.hookToQuery(categoryStreamCreator).subscribe((response) => 
    {
      this.category = response;
    });
  }

  createCategory() {
    this.selectedCategory = {} as TicketCategoryDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editCategory(id: string) {
    this.categoryService.get(id).subscribe((category) => {
      this.selectedCategory = category;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm(){
    this.form = this.fb.group({
      name: [this.selectedCategory.name || '', Validators.required],
      description: [this.selectedCategory.description || '']
    });
  }

  save() {
    if(this.form.invalid) {
      return;
    }

    if(this.selectedCategory.id) {
      this.categoryService.update(this.selectedCategory.id, this.form.value).subscribe(() =>{
        this.isModalOpen = false;
        this.form.reset();
        this.toaster.success('Category Updated!');
        this.list.get();
      });
    }

    else {
      this.categoryService.create(this.form.value).subscribe(() =>{
        this.isModalOpen = false;
        this.form.reset();
        this.toaster.success('Category Added!');
        this.list.get();
      });
    }
  }

  delete(id: string){
    this.confirmation.warn('::AreYouSureToDelete?', '::AreYouSure?').subscribe((status) =>
    {
      if(status === Confirmation.Status.confirm) {
        this.categoryService.delete(id).subscribe(()=> {
          this.toaster.success('Category Deleted!');
          this.list.get();
        });
      }
    });
  }
}
