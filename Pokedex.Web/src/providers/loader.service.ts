import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  private changeLoadingSource = new Subject<boolean>();

  changeLoading = this.changeLoadingSource.asObservable();

  constructor() {
   }

  show() {
    this.changeLoadingSource.next(true);
  }

  hide() {
    this.changeLoadingSource.next(false);
  }
}
