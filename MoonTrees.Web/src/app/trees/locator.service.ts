﻿import { Injectable } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class TreeService {
    url: string = `http://localhost:25000/api/trees`; // `https://moontrees.stillware.com/api/`;
    constructor(private http: Http) { }

    getTree(tree: string) {
        let params = new URLSearchParams();
        params.set('tree', tree);

        return this.http.get(this.url, { search: params })
            .map((res) => res.json());
    }
    getTrees() {
        return this.http.get(this.url)
            .map((res) => res.json());
    }
    searchTrees(filter: string, searchString: string) {
        let params = new URLSearchParams();
        params.set('filter', filter);
        params.set('postalCode', searchString);

        return this.http.get(`{this.url}search`, { search: params })
            .map((res) => res.json());
    }
}