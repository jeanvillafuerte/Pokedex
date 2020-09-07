import { Injectable } from '@angular/core';
import { CommonService } from './common.service';
import { HttpClient } from '@angular/common/http';
import { Pokemon, ResultPokedex } from 'src/Interfaces/IPokemon';
import { ConfigService } from './config.service';

@Injectable()
export class PokedexService {

    endPoint: string;

    constructor(
        private http: HttpClient,
        private commonSrv: CommonService,
        private configSrv: ConfigService
    ) {
        this.endPoint = 'api/pokedex';
    }

    findByName(name: string, page: number, pageSize: number) {
        const query = `${this.configSrv.BASE_URL}${this.endPoint}/search/${name}/${page}/${pageSize}`;
        return this.http.get<ResultPokedex>(query);
    }

    findById(id: number) {
        const query = `${this.configSrv.BASE_URL}${this.endPoint}/${id}`;
        return this.http.get<Pokemon>(query);
    }

}