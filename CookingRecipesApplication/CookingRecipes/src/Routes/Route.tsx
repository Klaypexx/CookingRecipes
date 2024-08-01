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
        path: 'favourites',
        element: (
          <ProtectedRoute>
            <RecipeView />
          </ProtectedRoute>
        ),
      },
    ],
  },
  {
    path: 'recipe',
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
  {
    path: 'recipe/:recipeId',
    errorElement: <ErrorPage />,
    element: (
      <ProtectedRoute>
        <RecipeView />
      </ProtectedRoute>
    ),
  },
  { path: '*', element: <Navigate to="/" replace /> },
]);
