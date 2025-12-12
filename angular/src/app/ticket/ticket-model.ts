import { Injectable } from '@angular/core';
import { RestService, Rest } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class FileAttachmentService {
  apiName = 'Default';
  
        uploadFile = (descriptionUrl: FormData, expectedBehaviourUrl: FormData, actualBehaviourUrl: FormData, nonWorkRoundUrl: FormData, id: string, config?: Partial<Rest.Config>) =>
          this.restService.request<any, void>({
            method: 'POST',
            url: `/api/app/tickets/upload-file/${id}`,
            body: nonWorkRoundUrl,
          },
          { apiName: this.apiName,...config });
  
    constructor(private restService: RestService) {}
}
