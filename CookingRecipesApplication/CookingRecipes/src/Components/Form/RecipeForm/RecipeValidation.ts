import * as Yup from 'yup';

const recipeValidation = Yup.object().shape({
  name: Yup.string().required('Название рецепта обязательно').max(50, 'Название не должно превышать 50 символов'),
  description: Yup.string()
    .required('Описание рецепта обязательно')
    .max(150, 'Описание не должно превышать 150 символов'),
  avatar: Yup.mixed()
    .nullable()
    .test('fileType', 'Неподдерживаемый формат файла', (value) => {
      return value instanceof File ? ['image/jpeg', 'image/png'].includes(value.type) : true;
    }),
  cookingTime: Yup.number().required('Время приготовления обязательно').min(1, 'Укажите количество минут'),
  portion: Yup.number().required('Количество порций обязательно').min(1, 'Укажите количество порций'),
  tags: Yup.array().min(1, 'Необходимо добавить хотя бы один тег'),
  steps: Yup.array()
    .of(
      Yup.object().shape({
        description: Yup.string()
          .required('Описание шага обязательно')
          .max(300, 'Описание шага не должно превышать 300 символов'),
      }),
    )
    .min(1, 'Необходимо добавить хотя бы один шаг'),
  ingredients: Yup.array()
    .of(
      Yup.object().shape({
        name: Yup.string()
          .required('Название ингредиента обязательно')
          .max(20, 'Название ингредиента не должно превышать 20 символов'),
        product: Yup.string().required('Продукт обязателен').max(300, 'Продукт не должен превышать 300 символов'),
      }),
    )
    .min(1, 'Необходимо добавить хотя бы один ингредиент'),
});

export default recipeValidation;
