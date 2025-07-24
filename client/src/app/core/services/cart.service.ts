import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Cart, CartItem } from '../../shared/models/cart';
import { Product } from '../../shared/models/product';
import { firstValueFrom, map, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  cart = signal<Cart | null>(null);
  itemCount = computed(() => {
    return (
      this.cart()?.items.reduce((sum, item) => sum + item.quantity, 0) ?? 0
    );
  });
  totals = computed(() => {
    const cart = this.cart();
    if (!cart) return { total: 0, shipping: 0, grandTotal: 0 };
    const subtotal = cart.items.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0
    );
    const shipping = subtotal > 100 ? 0 : 5;
    const discount = 0; // cart.couponCode ? 10 : 0; // Example discount logic
    const total = subtotal + shipping - discount;
    return { subtotal, shipping, discount, total };
  });

  getCart(id: string) {
    return this.http.get<Cart>(this.baseUrl + 'cart?id=' + id).pipe(
      map((cart) => {
        this.cart.set(cart);
        return cart;
      })
    );
  }

  setCart(cart: Cart) {
    return this.http.post<Cart>(this.baseUrl + 'cart', cart).pipe(
      tap((cart) => {
        this.cart.set(cart);
      })
    );
  }

  async addItemToCart(item: CartItem | Product, quantity = 1) {
    const cart = this.cart() ?? this.createCart();
    if (this.isProduct(item)) {
      item = this.mapProductToCartItem(item);
    }
    cart.items = this.addOrUpdateItem(cart.items, item, quantity);
    await firstValueFrom(this.setCart(cart));
  }

  async removeItemFromCart(productId: number, quantity = 1) {
    const cart = this.cart();
    if (!cart) return;
    const index = cart.items.findIndex((x) => x.productId === productId);
    if (index !== -1) {
      if (cart.items[index].quantity > quantity) {
        cart.items[index].quantity -= quantity;
      } else {
        cart.items.splice(index, 1);
      }
      if (cart.items.length === 0) {
        this.deleteCart();
      } else {
        await firstValueFrom(this.setCart(cart));
      }
    }
  }

  deleteCart() {
    this.http.delete(this.baseUrl + 'cart?id=' + this.cart()?.id).subscribe({
      next: () => {
        localStorage.removeItem('cart_id');
        this.cart.set(null);
      },
    });
  }

  private addOrUpdateItem(
    cartItems: CartItem[],
    item: CartItem,
    quantity: number
  ): CartItem[] {
    const existingItemIndex = cartItems.findIndex(
      (i) => i.productId === item.productId
    );
    if (existingItemIndex > -1) {
      cartItems[existingItemIndex].quantity += quantity;
    } else {
      cartItems.push(item);
    }

    return cartItems;
  }

  private isProduct(item: CartItem | Product): item is Product {
    return (item as Product).id !== undefined;
  }

  private mapProductToCartItem(item: Product): CartItem {
    return {
      productId: item.id,
      productName: item.name,
      price: item.price,
      quantity: 1, // Default quantity
      pictureUrl: item.pictureUrl,
      brand: item.brand,
      type: item.type,
    };
  }

  createCart(): Cart {
    const cart = new Cart();
    localStorage.setItem('cart_id', cart.id);
    return cart;
  }
}
