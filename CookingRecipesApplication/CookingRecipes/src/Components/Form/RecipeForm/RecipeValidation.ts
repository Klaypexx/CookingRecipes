import * as Yup from 'yup';
import {
  DESCRIPTION_MAX_WORDS,
  INGREDIENTS_NAME_MAX_WORDS,
  INGREDIENTS_PRODUCT_MAX_WORDS,
  NAME_MAX_WORDS,
  STEPS_DESCRIPTION_MAX_WORDS,
  TAG_MAX_WORDS,
  TAGS_MAX_COUNT,
} from '../../../Constants/recipe';

const recipeValidation = Yup.object().shape({
  name: Yup.string()
    .required('Название рецепта обязательно')
    .max(NAME_MAX_WORDS, `Название не должно превышать ${NAME_MAX_WORDS} символов`),
  description: Yup.string()
    .required('Описание рецепта обязательно')
    .max(DESCRIPTION_MAX_WORDS, `Описание не должно превышать ${DESCRIPTION_MAX_WORDS} символов`),
  avatar: Yup.mixed()
    .nullable()
    .test('fileType', 'Неподдерживаемый формат файла', (value) => {
      return value instanceof File ? ['image/jpeg', 'image/png'].includes(value.type) : true;
    }),
  cookingTime: Yup.number().required('Обязетальное поле').min(1, 'Укажите количество минут'),
  portion: Yup.number().required('Обязетальное поле').min(1, 'Укажите количество порций'),
  tags: Yup.array()
    .of(Yup.string().max(TAG_MAX_WORDS, `Тег не должен превышать ${TAG_MAX_WORDS} символов`))
    .max(TAGS_MAX_COUNT, `Не более ${TAGS_MAX_COUNT} тегов`),
  steps: Yup.array()
    .of(
      Yup.object().shape({
        description: Yup.string()
          .required('Описание шага обязательно')
          .max(
            STEPS_DESCRIPTION_MAX_WORDS,
            `Описание шага не должно превышать ${STEPS_DESCRIPTION_MAX_WORDS} символов`,
          ),
      }),
    )
    .min(1, 'Необходимо добавить хотя бы один шаг'),
  ingredients: Yup.array()
    .of(
      Yup.object().shape({
        name: Yup.string()
          .required('Название ингредиента обязательно')
          .max(
            INGREDIENTS_NAME_MAX_WORDS,
            `Название ингредиента не должно превышать ${INGREDIENTS_NAME_MAX_WORDS} символов`,
          ),
        product: Yup.string()
          .required('Продукт обязателен')
          .max(INGREDIENTS_PRODUCT_MAX_WORDS, `Продукт не должен превышать ${INGREDIENTS_PRODUCT_MAX_WORDS} символов`),
      }),
    )
    .min(1, 'Необходимо добавить хотя бы один ингредиент'),
});

export default recipeValidation;
