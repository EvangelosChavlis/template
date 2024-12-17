export interface ButtonProps {
    title: string;
    icon: React.ReactNode;
    color: "primary" | "secondary" | "success" | "danger" | "warning" | "info" | "light" | "dark"; 
    placement: "top" | "right" | "bottom" | "left";
    disabled: boolean;
    action: () => void;
}