// types.ts

export interface ContentTypeField {
    id: string;
    name: string;
    type: string;
    required: boolean;
  }
  
  export interface ContentType {
    sys: { id: string };
    name: string;
    fields: ContentTypeField[];
  }
  
  export interface Entry {
    sys: { id: string };
    fields: { [key: string]: any };
  }
  