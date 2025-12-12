import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppTextEditorComponent } from './app-text-editor.component';

const routes: Routes = [{ path: '', component: AppTextEditorComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppTextEditorRoutingModule { }
