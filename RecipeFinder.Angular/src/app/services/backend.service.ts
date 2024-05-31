import { Injectable } from '@angular/core';

@Injectable()
export class BackendService {
  public url: string = 'http://localhost:5274';
  constructor() {}
}
