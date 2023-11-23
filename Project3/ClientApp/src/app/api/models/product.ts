/* tslint:disable */
/* eslint-disable */
import { ProductType } from '../models/product-type';
export interface Product {
  cost?: number;
  description?: string | null;
  id?: string;
  image?: string | null;
  name?: string | null;
  productTypeId?: string;
  type?: ProductType;
}
