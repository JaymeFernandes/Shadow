export interface ApiResponse<T> {
  status: number;
  title?: string;
  data?: T;
  meta?: any;
}

export interface ProblemDetails {
  type?: string;
  title?: string;
  status?: number;
  detail?: string;
  instance?: string;
}
