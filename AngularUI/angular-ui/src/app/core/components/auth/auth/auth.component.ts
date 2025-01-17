import { Component, OnInit } from '@angular/core';
import { RouterService } from 'src/app/core/services/router.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
  constructor(private routerService: RouterService) {}

  ngOnInit(): void {
    this.routerService.pageNotFoundRoute();
  }
}
