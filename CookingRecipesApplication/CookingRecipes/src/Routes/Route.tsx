import { createBrowserRouter, Navigate } from 'react-router-dom';
import App from '../App';
import HomePage from '../Pages/HomePage/HomePage';
import ProtectedRoute from './ProtectedRoute';
import CreateRecipe from '../Pages/CreateRecipe/CreateRecipe';
import RecipesList from '../Pages/RecipesList/RecipesList';
import RecipeView from '../Pages/RecipeView/RecipeView';
import ErrorPage from '../Pages/ErrorPage/ErrorPage';

export const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      { path: '', element: <HomePage /> },
      {
        path: 'recipes',
        element: (
          <ProtectedRoute>
            <RecipesList />
          </ProtectedRoute>
        ),
      },
      {
        path: 'recipes/create',
        element: (
          <ProtectedRoute>
            <CreateRecipe />
          </ProtectedRoute>
        ),
      },
      {
        path: 'recipes/:recipeId',
        element: (
          <ProtectedRoute>
            <RecipeView />
          </ProtectedRoute>
        ),
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
  { path: '*', element: <Navigate to="/" replace /> },
]);
