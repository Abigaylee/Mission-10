import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      // Lets the React app call `/api/...` while forwarding to ASP.NET.
      '/api': {
        target: 'http://localhost:5209',
        changeOrigin: true,
      },
    },
  },
})
