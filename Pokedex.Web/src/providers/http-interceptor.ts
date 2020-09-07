import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HTTP_INTERCEPTORS, HttpResponse, HttpErrorResponse } from "@angular/common/http";
import { Injectable, NgModule } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { LoadingService } from './loader.service';
import { tap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { Router } from '@angular/router';

@Injectable(
)
export class HttpRequestInterceptor implements HttpInterceptor {

    constructor(
        private loadingSrv: LoadingService,
        private router: Router
    ) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const token = sessionStorage.getItem('bearerToken');

        if (token) {
            req = req.clone(
                {
                    headers: req.headers.set('Authorization', 'Bearer ' + token)
                });
        } else {

        }

        this.loadingSrv.show();

        return next.handle(req).pipe(
            tap(evt => {
                if (evt instanceof HttpResponse) {
                    this.loadingSrv.hide();
                }
            },
            error => {
                console.log(error);
                if (error.status === 401) {
                    this.loadingSrv.hide();
                    this.router.navigate(['login']);
                  }
            }),
            catchError(
                error => {
                    this.loadingSrv.hide();
                    return of(error);
                }
            ));
    }
};

@NgModule({
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: HttpRequestInterceptor,
            multi: true,
            deps: [LoadingService]
        }
    ]
})
export class HttpInterceptorModule { }