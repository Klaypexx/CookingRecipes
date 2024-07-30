import { createBrowserRouter, Navigate } from 'react-router-dom';
import App from '../App';
import HomePage from '../Pages/HomePage/HomePage';
import ProtectedRoute from './ProtectedRoute';
import CreateRecipe from '../Pages/CreateRecipe/CreateRecipe';

export const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: [
      { path: '', element: <HomePage /> },
      {
        path: 'recipes',
        element: <HomePage />,
      },
      {
        path: 'favourites',
        element: (
          <ProtectedRoute>
            <HomePage />
          </ProtectedRoute>
        ),
      },
    ],
  },
  {
    path: '/recipe',
    element: <App />,
    children: [
      {
        path: 'create',
        element: (
          <ProtectedRoute>
            <CreateRecipe />
          </ProtectedRoute>
        ),
      },
    ],
  },
  { path: '*', element: <Navigate to="/" replace /> },
]);
