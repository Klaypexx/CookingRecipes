import { createBrowserRouter, Navigate } from 'react-router-dom';
import App from '../App';
import HomePage from '../Pages/HomePage/HomePage';
import ProtectedRoute from './ProtectedRoute';
import CreateRecipe from '../Pages/CreateRecipe/CreateRecipe';
import RecipesList from '../Pages/RecipesList/RecipesList';
import RecipeView from '../Pages/RecipeView/RecipeView';
import ErrorPage from '../Pages/ErrorPage/ErrorPage';
import EditRecipe from '../Pages/EditRecipe/EditRecipe';
import Favourites from '../Pages/Favourites/Favourites';

export const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      { path: '', element: <HomePage /> },
      {
        path: 'recipes',
        element: <RecipesList />,
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
        element: <RecipeView />,
      },
      {
        path: 'recipes/edit/:recipeId',
        element: (
          <ProtectedRoute>
            <EditRecipe />
          </ProtectedRoute>
        ),
      },
      {
        path: 'favourites',
        element: (
          <ProtectedRoute>
            <Favourites />
          </ProtectedRoute>
        ),
      },
    ],
  },
  { path: '*', element: <Navigate to="/" replace /> },
]);
