import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor() { }

  public async imageToBase64Array(imageUrl: string): Promise<string> {
    const response = await fetch(imageUrl);
    const blob = await response.blob();
    const arrayBuffer = await blob.arrayBuffer();
    let imageBytes = new Uint8Array(arrayBuffer);
    let base64 = btoa(String.fromCharCode(...imageBytes));
    return base64;
  }

  base64ArrayToImage(base64: string): string {
    let bytes = atob(base64);
    let array = new Uint8Array(bytes.length);
    for (let i = 0; i < bytes.length; i++) {
      array[i] = bytes.charCodeAt(i);
    }

    const blob = new Blob([array], { type: 'image/jpeg' });
    return URL.createObjectURL(blob);
  }
}
