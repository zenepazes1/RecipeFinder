import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable()
export class CloudinaryService {
  constructor(private http: HttpClient) {}
  cloudinaryUrl: string = `https://api.cloudinary.com/v1_1/${environment.cloudinary.cloudName}/image/upload`;

  uploadImage(image: File): Promise<string> {
    const formData = new FormData();
    formData.append('file', image);
    formData.append('upload_preset', environment.cloudinary.uploadPreset);
    formData.append('cloud_name', environment.cloudinary.cloudName);

    return firstValueFrom(this.http.post(this.cloudinaryUrl, formData))
      .then((res: any) => {
        console.log(res.secure_url);
        return res.secure_url;
      })
      .catch((err) => {
        console.error(err);
      });
  }
}
