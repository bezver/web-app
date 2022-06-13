import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilmCreateEditComponent } from './film-create-edit.component';

describe('FilmCreateEditComponent', () => {
  let component: FilmCreateEditComponent;
  let fixture: ComponentFixture<FilmCreateEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FilmCreateEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FilmCreateEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
