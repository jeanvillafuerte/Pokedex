import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { tap } from 'rxjs/internal/operators/tap';
import { AppUserAuth, AppUser } from 'src/security/app-user';
import { CommonService } from './common.service';
import { SessionStorageService } from './sessionstorage.service';

@Injectable()
export class SecurityService {

    endPoint = 'api/login';

    public get securityObject(): AppUserAuth {
        const securityObject = this.storaSrv.getData('us');
        return securityObject ? securityObject : new AppUserAuth();
    }

    SetAppUserAuth(v: AppUserAuth) {
        this.storaSrv.saveData('us', v);
    }

    constructor(
        private http: HttpClient,
        private commonSrv: CommonService,
        private storaSrv: SessionStorageService
    ) {
    }

    login(user: AppUser): Observable<AppUserAuth> {

        this.resetSecurityObject();

        const httpOptionsLocal = {
            headers: new HttpHeaders({
                accept: 'application/json',
                'Content-Type': 'application/json; charset=utf-8'
            })
        };

        const query = this.commonSrv.buildApiUrl(this.endPoint);

        return this.http.post<AppUserAuth>(query, user, httpOptionsLocal).pipe(
            tap(
                res => {
                    console.log(res);
                    this.SetAppUserAuth(res);

                    if (res) {
                        this.storaSrv.saveData('bearerToken', res.bearerToken);
                    }

                }
            )
        );
    }


    logout(): void {
        this.resetSecurityObject();
    }

    resetSecurityObject(): void {

        this.securityObject.id = 0;
        this.securityObject.userName = '';
        this.securityObject.bearerToken = '';
        this.securityObject.isAuthenticated = false;

        this.storaSrv.remove('bearerToken');
        this.storaSrv.remove('us');
    }

}