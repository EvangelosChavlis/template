import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import path from 'path';

export default defineConfig({
  server: {
    port: 3000,
  },
  resolve: {
    alias: {
      src: path.resolve(__dirname, './src'), // Ensure absolute paths resolve correctly
    },
  },
  plugins: [react()],
});
