// import { FieldArray, FieldArrayRenderProps } from 'formik';

// const StepField: React.FC<StepFieldProps> = ({ name }) => {
//   const handlerCreateField = (arrayHelpers: FieldArrayRenderProps) => {
//     arrayHelpers.push({ header: '', products: '' });
//   };

//   const handlerDeleteCurrentField = (arrayHelpers: FieldArrayRenderProps, index: number) => {
//     if (index == 0) {
//       return;
//     }
//     arrayHelpers.remove(index);
//   };

//   return (
//     <FieldArray
//       name={name}
//       render={(arrayHelpers: FieldArrayRenderProps) => {
//         const ingredients = arrayHelpers.form.values[name] || [];
//         return (
//           <>
//             {ingredients.map((ingredient: { header: string; products: string }, index: number) => (
//               <div key={index}>
//                 <div className={styles.ingredientButtonBox}>
//                   <button
//                     type="button"
//                     className={styles.ingredientCloseButton}
//                     onClick={() => handlerDeleteCurrentField(arrayHelpers, index)}
//                   >
//                     <img src={closeIcon} alt="closeIcon" className={styles.ingredientCloseIcon} />
//                   </button>
//                 </div>
//                 <div className={styles.ingredientBox}>
//                   <FormField
//                     className={styles.inputIngredientNameFormSize}
//                     margin
//                     name={`${name}.${index}`}
//                     type="text"
//                     placeholder="Заголовок для ингридиентов"
//                   />
//                 </div>
//               </div>
//             ))}
//             <AddRecipeButton className={styles.ingredientButton} onClick={() => handlerCreateField(arrayHelpers)} />
//           </>
//         );
//       }}
//     />
//   );
// };

// export default IngredientField;
