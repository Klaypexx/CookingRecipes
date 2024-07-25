import React from 'react';
import styles from './TagsInput.module.css';
import closeIcon from '../../../resources/icons/closeTag.svg';
import { FieldArray, FieldArrayRenderProps } from 'formik';

// Define the props interface
interface TagsInputProps {
  name: string; // Change to accept the name prop for Formik
}

const TagsInput: React.FC<TagsInputProps> = ({ name }) => {
  return (
    <FieldArray
      name={name}
      render={(arrayHelpers: FieldArrayRenderProps) => {
        // Extract the tags array into a variable
        const tags = arrayHelpers.form.values[name] || [];

        // Handler to create a new tag
        const handleCreate = (e: React.KeyboardEvent<HTMLInputElement>) => {
          if (e.key === 'Enter') {
            e.preventDefault();
            const inputValue = (e.target as HTMLInputElement).value.trim();
            if (inputValue) {
              arrayHelpers.push(inputValue);
              (e.target as HTMLInputElement).value = '';
            }
          }
        };

        // Handler to delete a tag
        const handleDelete = (index: number) => {
          arrayHelpers.remove(index);
        };

        return (
          <div className={styles.tagsInputBox}>
            <div className={styles.tagsContainer}>
              {tags.length > 0
                ? tags.map((tag: string, index: number) => (
                    <div key={index} className={styles.tag}>
                      <span className={styles.name}>{tag}</span>
                      <img
                        src={closeIcon}
                        alt="closeIcon"
                        className={styles.icon}
                        onClick={() => handleDelete(index)}
                      />
                    </div>
                  ))
                : null}
              <input
                className={styles.tagInput}
                type="text"
                placeholder="Добавить теги"
                maxLength={15}
                onKeyDown={handleCreate}
              />
            </div>
          </div>
        );
      }}
    />
  );
};

export default TagsInput;
