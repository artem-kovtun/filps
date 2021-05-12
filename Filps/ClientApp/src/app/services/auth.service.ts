import {Injectable} from '@angular/core';
import {FacebookLoginProvider, GoogleLoginProvider, SocialAuthService} from 'angularx-social-login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private socialAuthService: SocialAuthService) {
    this.socialAuthService.authState.subscribe((user) => {
      if (user !== null) {
        localStorage.setItem('token', user.idToken);
      }
    });
  }

  isAuthorized = () => {
    return localStorage.getItem('token') !== null;
  }

  loginWithGoogle = () => {
    const googleLoginOptions = {scope: 'profile email'};
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID, googleLoginOptions);
  }

  loginWithFacebook(): void {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  authState = () => {
    return this.socialAuthService.authState;
  }

  logOut = () => {
    return this.socialAuthService.signOut().then(_ => {
      localStorage.removeItem('token');
    });
  }

}
