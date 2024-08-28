export default interface BaseFormProps {
  primary?: boolean;
  id?: string;
  initialValues: any;
  validationSchema?: any;
  onSubmit: (values: any) => void;
  children?: React.ReactNode;
}
