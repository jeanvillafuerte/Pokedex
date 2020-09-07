import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable()
export class ConfigService {

public BASE_URL: string;

constructor() {
    this.BASE_URL = environment.apiUrl;
}

}
