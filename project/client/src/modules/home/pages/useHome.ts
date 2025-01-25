import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import { clearData, seedData } from 'src/modules/home/api/api';

const useHome = () => {
  const navigate = useNavigate();
  
  const handleClearData = async () => {
    const result = await clearData();
    toast.success(result.data);
    setTimeout(() => {
        navigate("/");
    }, 2000);
  }

  const handleSeedData = async () => {
    const result = await seedData();
    toast.success(result.data);
    setTimeout(() => {
        navigate("/");
    }, 2000);
  }
  
  return {
    handleClearData,
    handleSeedData
  }
}

export default useHome