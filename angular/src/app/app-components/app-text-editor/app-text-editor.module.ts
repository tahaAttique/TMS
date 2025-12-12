import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AppTextEditorRoutingModule } from './app-text-editor-routing.module';
import { AppTextEditorComponent } from './app-text-editor.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    AppTextEditorComponent
  ],
  imports: [
    CommonModule,
    AppTextEditorRoutingModule,
    SharedModule,
  ],
  exports: [
    AppTextEditorComponent
  ]

})
export class AppTextEditorModule { }
