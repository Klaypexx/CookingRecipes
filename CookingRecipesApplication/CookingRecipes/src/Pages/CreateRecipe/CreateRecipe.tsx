import React from 'react';
import BaseCard from '../../Components/Card/BaseCard/BaseCard';
import BaseForm from '../../Components/Form/BaseForm/BaseForm';
import FormInput from '../../Components/Form/FormInput/FormInput';
import TagsInput from '../../Components/Form/TagsInput/TagsInput';
import Subheader from '../../Components/Subheader/Subheader';
import styles from './CreateRecipe.module.css';
import { RecipeFormValues } from '../../Types/types';

const CreateRecipe: React.FC = () => {
  const initialValues: RecipeFormValues = {
    recipeName: '',
    description: '',
    tags: [],
    cookingTime: '',
    portion: '',
    step: '',
    ingredient: {
      header: '',
      products: '',
    },
  };

  const handleSubmit = (values: RecipeFormValues) => {
    console.log(values);
  };

  return (
    <section className={styles.formSection}>
      <BaseForm initialValues={initialValues} onSubmit={handleSubmit}>
        <Subheader backward btn type="submit" buttonText="Опубликовать" headerText="Добавить новый рецепт" />
        <BaseCard margin>
          <div className={styles.mainFormBlock}>
            <FormInput
              className={styles.inputFormSize}
              margin
              name="recipeName"
              type="text"
              placeholder="Название рецепта"
            />
            <FormInput
              margin
              className={styles.textareaFormSize}
              as="textarea"
              name="description"
              placeholder="Краткое описание рецепта (150 символов)"
              maxLength={150}
            />
            <TagsInput name="tags" />
            <div className={styles.optionContainer}>
              <div className={styles.optionBox}>
                <FormInput select as="select" name="cookingTime" className={styles.optionForm}>
                  <option value="">Время готовки</option>
                  <option value="5">5</option>
                  <option value="10">10</option>
                  <option value="15">15</option>
                  <option value="30">30</option>
                  <option value="35">35</option>
                  <option value="40">40</option>
                  <option value="60">60</option>
                </FormInput>
                <p className={styles.optionText}>Минут</p>
              </div>
              <div className={styles.optionBox}>
                <FormInput select as="select" name="portion" className={styles.optionForm}>
                  <option value="">Порций в блюде</option>
                  <option value="1">1</option>
                  <option value="2">2</option>
                  <option value="3">3</option>
                  <option value="4">4</option>
                  <option value="5">5</option>
                </FormInput>
                <p className={styles.optionText}>Персон</p>
              </div>
            </div>
          </div>
        </BaseCard>
      </BaseForm>
    </section>
  );
};

export default CreateRecipe;
