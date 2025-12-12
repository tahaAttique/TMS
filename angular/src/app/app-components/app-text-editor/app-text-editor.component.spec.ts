import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppTextEditorComponent } from './app-text-editor.component';

describe('AppTextEditorComponent', () => {
  let component: AppTextEditorComponent;
  let fixture: ComponentFixture<AppTextEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppTextEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppTextEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
