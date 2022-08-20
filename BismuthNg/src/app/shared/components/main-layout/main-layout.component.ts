import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { Issue } from "../../interfaces";

@Component({
  selector: "app-main-layout",
  templateUrl: "./main-layout.component.html",
  styleUrls: ["./main-layout.component.scss"],
})
export class MainLayoutComponent implements OnInit {
  public projects = ["Initial", "Another", "Main", "Test"];

  project$!: Observable<Issue>;

  public authUser: string = "Bill Wurtz";

  constructor() {}

  ngOnInit(): void {}
}
