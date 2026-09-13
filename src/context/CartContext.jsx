import { createContext, useContext, useEffect, useState } from 'react'
import { shopiloApi } from '../api/shopiloApi'

const CART_ID_KEY = 'shopilo-cart-id'
const CartContext = createContext(null)

function mapCart(cartDto) {
  return cartDto.cartItems.map(item => ({
    id: item.productId,
    cartItemId: item.id,
    title: item.productTitle,
    brand: item.brand,
    thumbnail: item.thumbnail,
    price: item.unitPrice,
    quantity: item.quantity,
    stock: item.stock,
  }))
}

export function CartProvider({ children }) {
  const [cartId, setCartId] = useState(() => localStorage.getItem(CART_ID_KEY))
  const [cart, setCart] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    async function loadCart() {
      try {
        let currentCartId = cartId

        if (!currentCartId) {
          const createdCart = await shopiloApi.createCart()
          currentCartId = createdCart.id
          localStorage.setItem(CART_ID_KEY, currentCartId)
          setCartId(currentCartId)
          setCart([])
          return
        }

        const existingCart = await shopiloApi.getCart(currentCartId)
        setCart(mapCart(existingCart))
      } catch {
        localStorage.removeItem(CART_ID_KEY)
        const createdCart = await shopiloApi.createCart()
        localStorage.setItem(CART_ID_KEY, createdCart.id)
        setCartId(createdCart.id)
        setCart([])
      } finally {
        setLoading(false)
      }
    }

    loadCart().catch(requestError => {
      setError(requestError.message)
      setLoading(false)
    })
  }, [])

  async function addToCart(product, quantity = 1) {
    if (!cartId) return
    try {
      setError(null)
      const updatedCart = await shopiloApi.addCartItem(cartId, product.id, quantity)
      setCart(mapCart(updatedCart))
    } catch (requestError) {
      setError(requestError.message)
    }
  }

  async function removeFromCart(productId) {
    const item = cart.find(cartItem => cartItem.id === productId)
    if (!cartId || !item) return
    const updatedCart = await shopiloApi.removeCartItem(cartId, item.cartItemId)
    setCart(mapCart(updatedCart))
  }

  async function updateQty(productId, quantity) {
    const item = cart.find(cartItem => cartItem.id === productId)
    if (!cartId || !item) return
    const updatedCart = await shopiloApi.updateCartItem(cartId, item.cartItemId, quantity)
    setCart(mapCart(updatedCart))
  }

  async function clearCart() {
    if (!cartId) return
    await shopiloApi.clearCart(cartId)
    setCart([])
  }

  async function restoreCart(items) {
    for (const item of items) {
      await shopiloApi.addCartItem(cartId, item.id, item.quantity)
    }
    const restoredCart = await shopiloApi.getCart(cartId)
    setCart(mapCart(restoredCart))
  }

  const cartCount = cart.reduce((sum, item) => sum + item.quantity, 0)
  const cartTotal = cart.reduce((sum, item) => sum + item.price * item.quantity, 0)
  const isInCart = id => cart.some(item => item.id === id)

  return (
    <CartContext.Provider value={{
      cartId,
      cart,
      cartCount,
      cartTotal,
      loading,
      error,
      addToCart,
      removeFromCart,
      updateQty,
      clearCart,
      restoreCart,
      isInCart,
    }}>
      {children}
    </CartContext.Provider>
  )
}

export function useCart() {
  const context = useContext(CartContext)
  if (!context) throw new Error('useCart must be used inside CartProvider')
  return context
}
