import { Injectable } from '@angular/core';
import { CommonService } from './common.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Pokemon, ResultPokedex } from 'src/Interfaces/IPokemon';
import { ConfigService } from './config.service';

@Injectable()
export class FavoritesService {

    endPoint: string;

    constructor(
        private http: HttpClient,
        private commonSrv: CommonService,
        private configSrv: ConfigService
    ) {
        this.endPoint = 'api/pokemon';
    }

    getFavorites(idUser: number, page: number, pageSize: number) {
        const query = `${this.configSrv.BASE_URL}${this.endPoint}/favorites/${idUser}/${page}/${pageSize}`;
        return this.http.get<ResultPokedex>(query);
    }

    favorite(idUser: number, idPokemon: number, isFavorite: boolean) {
        let query = `${this.configSrv.BASE_URL}${this.endPoint}`;

        if (!isFavorite) {
            const httpOptions = {
                headers: new HttpHeaders({
                    'Content-Type': 'application/json'
                })
            };

            return this.http.post(query, { idUser, idPokemon }, httpOptions);

        } else {

            query = `${this.configSrv.BASE_URL}${this.endPoint}/${idUser}/${idPokemon}`;

            return this.http.delete(query);
        }

    }
}