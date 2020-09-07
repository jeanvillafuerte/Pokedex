import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { DomSanitizer } from '@angular/platform-browser';

@Injectable()
export class CommonService {

    constructor(private configSrv: ConfigService,
                private sanitizer: DomSanitizer) { }

    buildApiUrl(endPoint: string): string {
        return this.configSrv.BASE_URL + endPoint;
    }
    sanitizeBase64Image(url: string) {
        return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + url);
    }
}
