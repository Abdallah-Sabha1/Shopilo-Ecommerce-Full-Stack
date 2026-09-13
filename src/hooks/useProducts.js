import { useState, useEffect } from 'react'
import { shopiloApi } from '../api/shopiloApi'

export function useProducts({ category = '', limit = 100, skip = 0 } = {}) {
  const [products, setProducts] = useState([])
  const [total, setTotal] = useState(0)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    let cancelled = false

    async function fetchProducts() {
      try {
        setLoading(true)
        setError(null)

        const query = category
          ? `?category=${encodeURIComponent(category)}`
          : ''

        const data = await shopiloApi.getProducts(query)
        if (!cancelled) {
          const page = data.slice(skip, skip + limit)
          setProducts(page)
          setTotal(data.length)
        }
      } catch (requestError) {
        if (!cancelled) setError(requestError.message)
      } finally {
        if (!cancelled) setLoading(false)
      }
    }

    fetchProducts()
    return () => { cancelled = true }
  }, [category, limit, skip])

  return { products, total, loading, error }
}

export function useProduct(id) {
  const [product, setProduct] = useState(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    if (!id) return undefined
    let cancelled = false

    async function fetchProduct() {
      try {
        setLoading(true)
        setError(null)
        const data = await shopiloApi.getProduct(id)
        if (!cancelled) setProduct(data)
      } catch (requestError) {
        if (!cancelled) setError(requestError.message)
      } finally {
        if (!cancelled) setLoading(false)
      }
    }

    fetchProduct()
    return () => { cancelled = true }
  }, [id])

  return { product, loading, error }
}

export function useCategories() {
  const [categories, setCategories] = useState([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    shopiloApi.getCategories()
      .then(data => setCategories(data.map(category => ({
        ...category,
        slug: category.name.toLowerCase().replace(/\s+/g, '-'),
      }))))
      .finally(() => setLoading(false))
  }, [])

  return { categories, loading }
}

export function useSearch(query) {
  const [results, setResults] = useState([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)

  useEffect(() => {
    if (!query || query.trim().length < 2) {
      setResults([])
      return undefined
    }

    let cancelled = false

    async function search() {
      try {
        setLoading(true)
        setError(null)
        const data = await shopiloApi.getProducts(`?search=${encodeURIComponent(query)}`)
        if (!cancelled) setResults(data)
      } catch (requestError) {
        if (!cancelled) setError(requestError.message)
      } finally {
        if (!cancelled) setLoading(false)
      }
    }

    search()
    return () => { cancelled = true }
  }, [query])

  return { results, loading, error }
}
