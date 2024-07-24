/* eslint-disable @typescript-eslint/no-explicit-any */
import React from "react";
import BaseCard from "../../Components/Card/BaseCard/BaseCard";
import BaseForm from "../../Components/Form/BaseForm/BaseForm";
import FormInput from "../../Components/Form/FormInput/FormInput";
import TagsInput from "../../Components/Form/TagsInput/TagsInput";
import Subheader from "../../Components/Subheader/Subheader";
import styles from "./CreateRecipe.module.css";

// Define the shape of the form values
interface FormValues {
    recipeName: string;
    description: string;
    tags: string[];
}

const CreateRecipe: React.FC = () => {
    
    const initialValues: FormValues = {
        recipeName: "",
        description: "",
        tags: ['awda', 'awdawd'],
    };

    const handleSubmit = (values: FormValues) => {
        console.log(values.tags);
    };

    return (
        <section className={styles.formSection}>
            <BaseForm 
                initialValues={initialValues}
                onSubmit={handleSubmit} // Use handleSubmit instead of onSubmit
            >
                <Subheader backward btn type="submit" buttonText="Опубликовать" headerText="Добавить новый рецепт" />
                <BaseCard>
                    <div className={styles.mainFormBlock}>
                        <FormInput 
                            className={styles.inputFormSize} 
                            margin 
                            name="recipeName" 
                            type="text" 
                            placeholder="Название рецепта"
                        />
                        <FormInput 
                            className={styles.textareaFormSize}
                            as="textarea"
                            name="description" // Ensure this matches the initial values
                            placeholder="Краткое описание рецепта (150 символов)"
                        />
                        <TagsInput tags={initialValues.tags}/> {/* Ensure TagsInput has a name prop */}
                    </div>
                </BaseCard>
            </BaseForm>
        </section>
        // <MyForm />
    );
};

export default CreateRecipe;