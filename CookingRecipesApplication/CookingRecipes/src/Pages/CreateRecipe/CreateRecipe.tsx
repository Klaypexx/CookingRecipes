import React from 'react';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import RecipeForm from '../../Components/Form/RecipeForm/RecipeForm';
import Subheader from '../../Components/Subheader/Subheader';
import RecipeService from '../../Services/RecipeService';

const CreateRecipe: React.FC = () => {
  return (
    <>
      <section>
        <Subheader backward text="Редактировать рецепт">
          <BaseButton primary type="submit" form="form-submit" buttonText="Опубликовать" />
        </Subheader>
      </section>
      <section>
        <RecipeForm onSubmit={RecipeService.createRecipe} toastMessage="Рецепт успешно создан" />
      </section>
    </>
  );
};

export default CreateRecipe;
