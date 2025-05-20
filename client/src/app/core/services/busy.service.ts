import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  loading: boolean = false;
  busyRequestCount: number = 0;
  busy() {
    this.loading = true;
    this.busyRequestCount++;
  }

  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.loading = false;
    }
  }
}
