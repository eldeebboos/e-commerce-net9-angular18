import { nanoid } from 'nanoid';
export type CartType = {
  id: string;
  items: CartItem[];
};
export type CartItem = {
  productId: number;
  productName: string;
  quantity: number;
  price: number;
  pictureUrl: string;
  brand: string;
  type: string;
};
export class Cart implements CartType {
  id = nanoid();
  items: CartItem[] = [];

  //   addItem(item: CartItem) {
  //     const existingItem = this.items.find((i) => i.productId === item.productId);
  //     if (existingItem) {
  //       existingItem.quantity += item.quantity;
  //     } else {
  //       this.items.push(item);
  //     }
  //   }

  //   removeItem(productId: number) {
  //     this.items = this.items.filter((item) => item.productId !== productId);
  //   }

  //   clear() {
  //     this.items = [];
  //   }
}
