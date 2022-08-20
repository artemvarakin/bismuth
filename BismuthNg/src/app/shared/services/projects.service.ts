import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Project } from "../interfaces";

import { environment } from "../../../environments/environment";

@Injectable({
  providedIn: "root",
})
export class ProjectsService {
  constructor(private http: HttpClient) {}

  getAll(): Observable<Project[]> {
    return this.http.get<Project[]>(`${environment.bismuthApiUrl}/project/all`);
  }
}
