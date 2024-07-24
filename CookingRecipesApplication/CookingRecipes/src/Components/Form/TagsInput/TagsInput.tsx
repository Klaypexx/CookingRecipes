import React, { useState } from 'react';
import "./TagsInput.css"
import { FieldArray, FieldArrayRenderProps } from 'formik';

// Define the props interface
interface TagsInputProps {
  tags: string[];
}

const TagsInput: React.FC<TagsInputProps> = ({ tags }) => {
  const [currentTags, setCurrentTags] = useState<string[]>(tags);

  const handleInputKeyPress = (
    arrayHelpers: FieldArrayRenderProps, 
    event: React.KeyboardEvent<HTMLInputElement>
  ): void => {
    if (event.key === 'Enter') {
      event.preventDefault(); // Prevent form submission
      const inputValue = (event.target as HTMLInputElement).value.trim(); // Get input value

      if (inputValue) {
        arrayHelpers.push(inputValue); // Add the tag to the array
        setCurrentTags([...currentTags, inputValue]); // Update local state
        (event.target as HTMLInputElement).value = ''; // Clear the input
        console.log('Enter pressed in input');
      }
    }
  };

  return (
    <FieldArray
      name="tags"
      render={(arrayHelpers) => (
        <div className="tags-input">
          <ul id="tags">
            {currentTags && currentTags.length > 0 ? (
              currentTags.map((tag, index) => (
                <li key={index} className="tag">
                  <span className='tag-title'>{tag}</span>
                  <span className='tag-close-icon' onClick={() => {
                    arrayHelpers.remove(index); // Remove the tag
                    setCurrentTags(currentTags.filter((_, i) => i !== index)); // Update local state
                  }}>
                    x
                  </span>
                </li>
              ))
            ) : null}
          </ul>
          <input
            className='input'
            type="text"
            placeholder="Press enter to add tags"
            onKeyPress={e => handleInputKeyPress(arrayHelpers, e)}
          />
        </div>
      )}
    />
  );
};

export default TagsInput;
