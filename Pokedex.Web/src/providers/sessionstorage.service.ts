import { Injectable } from '@angular/core';

@Injectable()

export class SessionStorageService {

    saveData(key: string, value: any) {

        sessionStorage.removeItem(key);

        let internalData: any;

        if (value instanceof Object) {
            internalData = JSON.stringify(value);
        } else {
            internalData = value;
        }

        sessionStorage.setItem(key, internalData);

    }

    getData(key: string): any {
        const data = sessionStorage.getItem(key);

        try {
            const parseData = JSON.parse(data);
            return parseData;
        } catch (e) {
            return data;
        }
    }

    removeAll() {
        sessionStorage.clear();
    }

    remove(valor: string) {
        sessionStorage.removeItem(valor);
    }
}
