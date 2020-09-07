import { OnDestroy, AfterViewInit, Component } from '@angular/core';
import { LoadingService } from 'src/providers/loader.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-loader',
    templateUrl: './loader.component.html',
    styleUrls: ['./loader.component.css']
  })
export class LoaderComponent implements AfterViewInit, OnDestroy {

    loading = false;
    loadingSubscription: Subscription;

    constructor(private loaderSrv: LoadingService) {
    }

    ngAfterViewInit(): void {
        this.loadingSubscription = this.loaderSrv.changeLoading.subscribe(
            (res) => {
              this.loading = res;
            }
          );
    }

    ngOnDestroy(): void {
        this.loadingSubscription.unsubscribe();
    }
}
