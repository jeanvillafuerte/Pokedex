import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { SecurityService } from 'src/providers/security.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  userName = '';
  isMain = true;
  isLoginPage = true;
  isAuth = false;
  @ViewChild('navBurger') navBurger: ElementRef;
  @ViewChild('navMenu') navMenu: ElementRef;

  constructor(private router: Router,
              private securitySrv: SecurityService) {
    router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        this.isMain = val.url !== '/favorites';
      }
      const obj = this.securitySrv.securityObject;

      if (val instanceof NavigationEnd) {
        this.isLoginPage = val.url === '/login';
      }

      this.isAuth = obj.isAuthenticated;
      this.userName = obj.userName;
    });
  }

  toggleNavbar() {
    this.navBurger.nativeElement.classList.toggle('is-active');
    this.navMenu.nativeElement.classList.toggle('is-active');
  }

  logout() {
    this.securitySrv.logout();
    this.router.navigate(['/login']);
  }

}
