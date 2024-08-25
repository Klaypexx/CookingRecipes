import React from 'react';
import RecipeForm from '../../Components/Form/RecipeForm/RecipeForm';
import RecipeService from '../../Services/RecipeService';
import Subheader from '../../Components/Subheader/Subheader';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';

const CreateRecipe: React.FC = () => {
  return (
    <section>
      <Subheader backward text="Редактировать рецепт">
        <BaseButton primary type="submit" form="form-submit" buttonText="Опубликовать" />
      </Subheader>
      <RecipeForm onSubmit={RecipeService.createRecipe} toastMessage="Рецепт успешно создан" />
    </section>
  );
};

export default CreateRecipe;
