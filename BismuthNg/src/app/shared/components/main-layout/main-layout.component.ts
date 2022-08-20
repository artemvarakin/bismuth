import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { Project } from "../../interfaces";
import { ProjectsService } from "../../services/projects.service";

@Component({
  selector: "app-main-layout",
  templateUrl: "./main-layout.component.html",
  styleUrls: ["./main-layout.component.scss"],
})
export class MainLayoutComponent implements OnInit {
  projects$!: Observable<Project[]>;

  authUser: string = "Bill Wurtz";

  constructor(private projectsService: ProjectsService) {}

  ngOnInit(): void {
    this.projects$ = this.projectsService.getAll();
  }
}
