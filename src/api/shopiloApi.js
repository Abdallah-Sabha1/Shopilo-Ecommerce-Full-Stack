const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5183/api'

async function request(path, options = {}) {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    ...options,
    credentials: 'include',
    headers: {
      'Content-Type': 'application/json',
      ...options.headers,
    },
  })

  if (!response.ok) {
    const problem = await response.json().catch(() => null)
    throw new Error(problem?.detail ?? problem?.title ?? `Request failed (${response.status})`)
  }

  return response.status === 204 ? null : response.json()
}

export const shopiloApi = {
  getProducts(searchParams = '') {
    return request(`/products${searchParams}`)
  },

  getProduct(productId) {
    return request(`/products/${productId}`)
  },

  getCategories() {
    return request('/categories')
  },

  createCart() {
    return request('/carts', { method: 'POST' })
  },

  getCart(cartId) {
    return request(`/carts/${cartId}`)
  },

  addCartItem(cartId, productId, quantity) {
    return request(`/carts/${cartId}/items`, {
      method: 'POST',
      body: JSON.stringify({ productId, quantity }),
    })
  },

  updateCartItem(cartId, cartItemId, quantity) {
    return request(`/carts/${cartId}/items/${cartItemId}`, {
      method: 'PUT',
      body: JSON.stringify({ quantity }),
    })
  },

  removeCartItem(cartId, cartItemId) {
    return request(`/carts/${cartId}/items/${cartItemId}`, { method: 'DELETE' })
  },

  clearCart(cartId) {
    return request(`/carts/${cartId}/items`, { method: 'DELETE' })
  },

  placeOrder(cartId, couponCode) {
    return request('/orders', {
      method: 'POST',
      body: JSON.stringify({ cartId, couponCode }),
    })
  },
}
