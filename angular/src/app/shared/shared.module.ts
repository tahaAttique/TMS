import { CoreModule } from '@abp/ng.core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { PageModule } from '@abp/ng.components/page';
import { ThemeBasicModule } from '@abp/ng.theme.basic';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { QuillModule } from 'ngx-quill';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzCollapseModule } from 'ng-zorro-antd/collapse';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';

@NgModule({
  declarations: [],
  imports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    PageModule,
    ThemeBasicModule,
    NzSelectModule,
    QuillModule.forRoot(),
    QuillModule,
    NzTabsModule,
    NzCollapseModule,
    NzToolTipModule, 
    NzPaginationModule
  ],
  exports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    PageModule,
    ThemeBasicModule,
    NzSelectModule,
    QuillModule,
    NzTabsModule,
    NzCollapseModule,
    NzToolTipModule,
    NzPaginationModule  
  ],
  providers: []
})
export class SharedModule {}
