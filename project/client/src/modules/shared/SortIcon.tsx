// Define the functional component with props
interface SortIconProps {
  column: string;
  sortBy: string;
  sortOrder: "asc" | "desc";
}

const SortIcon = ({ column, sortBy, sortOrder }: SortIconProps) => {
    if (sortBy === column) {
        return sortOrder === "asc" ? <i className="bi bi-arrow-up-short" /> : <i className="bi bi-arrow-down-short" />;
    }
    return <i className="bi bi-sort" />;
};

export default SortIcon;
