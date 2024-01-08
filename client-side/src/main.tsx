import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './app/layout/App.tsx'
import {BrowserRouter} from "react-router-dom";
import {AuthProvider} from "./app/hooks/AuthContext.tsx";

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <AuthProvider>
          <BrowserRouter>
                <App />
          </BrowserRouter>
      </AuthProvider>
  </React.StrictMode>,
)
