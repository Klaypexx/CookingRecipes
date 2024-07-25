import React from 'react';
import styles from './TagsInput.module.css';
import { FieldArray, FieldArrayRenderProps } from 'formik';

// Define the props interface
interface TagsInputProps {
  name: string; // Change to accept the name prop for Formik
}

const TagsInput: React.FC<TagsInputProps> = ({ name }) => {
  return (
    <FieldArray
      name={name}
      render={(arrayHelpers: FieldArrayRenderProps) => (
        <div className={styles.tagsInput}>
          <ul className={styles.tagsBox}>
            {arrayHelpers.form.values[name] && arrayHelpers.form.values[name].length > 0
              ? arrayHelpers.form.values[name].map((tag: string, index: number) => (
                  <li key={index} className={styles.tag}>
                    <span className={styles.tagTitle}>{tag}</span>
                    <span className={styles.tagCloseIcon} onClick={() => arrayHelpers.remove(index)}>
                      x
                    </span>
                  </li>
                ))
              : null}
          </ul>
          <input
            className={styles.input}
            type="text"
            placeholder="Добавить теги"
            onKeyDown={(e) => {
              if (e.key === 'Enter') {
                e.preventDefault();
                const inputValue = (e.target as HTMLInputElement).value.trim();
                if (inputValue) {
                  arrayHelpers.push(inputValue);
                  (e.target as HTMLInputElement).value = '';
                }
              }
            }}
          />
        </div>
      )}
    />
  );
};

export default TagsInput;
