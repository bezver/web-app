import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-film-create-edit',
  templateUrl: './film-create-edit.component.html',
  styleUrls: ['./film-create-edit.component.scss']
})
export class FilmCreateEditComponent implements OnInit {
  isVisible: boolean;
  currentID: number;
  firstName: string;
  lastName: string;

  form: FormGroup;

  @Input()
  get popupVisible() {
    return this.isVisible;
  }
  set popupVisible(val) {
    this.isVisible = val;
    this.popupVisibleChange.emit(this.isVisible);
  }

  @Output() popupVisibleChange = new EventEmitter<boolean>();

  @Input()
  id: string;

  // constructor(private service: Service) {
  //   this.ds = service.getCustomers();
  // }

  ngOnInit() {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
    });
  }


  onShowing(): void {
    console.log('yes');
    // if (this.currentID !== -1) {
    //   const item = Query(this.ds)
    //     .filter(['ID', '=', this.currentID])
    //     .toArray()[0];
    //   this.firstName = item.FirstName;
    //   this.lastName = item.LastName;
    // } else {
    //   this.firstName = '';
    //   this.lastName = '';
    // }

  }
  submit(): void {
    // if (this.currentID !== -1) {
    //   this.service.editCustomer(this.currentID, this.firstName, this.lastName);
    //   this.popupVisible = false;
    // } else {
    //   const item = new Customer();
    //   item.FirstName = this.firstName;
    //   item.LastName = this.lastName;
    //   Query(this.ds)
    //     .max('ID')
    //     .then(result => {
    //       item.ID = result ? +result + 1 : 1;
    //     });
    //   this.service.addCustomer(item);
    //   this.popupVisible = false;
    // }
  }

  onCancelClick(): void {
    this.popupVisible = false;
  }




}
